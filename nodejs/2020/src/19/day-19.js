'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input.split('\n\n');
}

function createRuleMap(ruleList) {
  return new Map(ruleList.split('\n').map(r => r.split(': ')));
}

function flatten(array) {
  if (!Array.isArray(array)) return array;
  return [].concat(...array.map(flatten));
}

function layoutRules(ruleMap, rule, message) {
  if (rule.startsWith('"')) {
    return rule[1];
  } else if (/^\d+$/.test(rule)) {
    return layoutRules(ruleMap, ruleMap.get(rule))
  } else if (/\|/.test(rule)) {
    return `(${rule.split(' | ').map(sr => layoutRules(ruleMap, sr)).join('|')})`;
  } else {
    return rule.split(' ').map(sr => layoutRules(ruleMap, sr)).join('');
  }
}

function matchRule(ruleMap, rule, message) {
  if (rule.startsWith('"')) {
    if (message[0] === rule[1]) {
      return [message.slice(1)];
    } else {
      return [];
    }
  } else if (/^(\d+)$/.test(rule)) {
    return matchRule(ruleMap, ruleMap.get(rule), message);
  } else if (/\|/.test(rule)) {
    const subRules = rule.split(' | ');
    return flatten(subRules.map(subRule => matchRule(ruleMap, subRule, message)));
  } else {
    const subRules = rule.split(' ');
    let result = [message];
    for (let subRule of subRules) {
      result = flatten(result.map(mp => matchRule(ruleMap, subRule, mp)));
    }
    return result;
  }
}


parts.part1 = async function() {
  const [rules, messages] = parseInput(await readInput());
  const ruleMap = createRuleMap(rules);
  const messageList = messages.split('\n');
  const pattern = new RegExp(`^${layoutRules(ruleMap, ruleMap.get('0'))}$`);

  return messageList.filter(m => pattern.test(m)).length;
};

parts.part2 = async function() {
  const [rules, messages] = parseInput(await readInput());
  const ruleMap = createRuleMap(rules)
  const messageList = messages.split('\n');
  ruleMap.set('8', '42 | 42 8');
  ruleMap.set('11', '42 31 | 42 11 31');

  // const pattern = new RegExp(`^${layoutRules(ruleMap, ruleMap.get('0'))}$`);
  //
  // console.log(messageList.filter(m => pattern.test(m)).length);


  return messageList.filter(message => {
    const t = matchRule(ruleMap, '0', message);
    return t.includes('');
  }).length;
};

(async () => {
  console.log(await parts.part2());
})();
