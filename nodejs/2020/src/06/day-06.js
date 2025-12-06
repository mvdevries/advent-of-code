'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split('\n');
}

function reduceAnswers(lines) {
  return lines.reduce((acc, line) => {
    if (acc.length === 0 || line === '') {
      acc.push(line);
    } else {
      acc[acc.length - 1] += line;
    }

    return acc;
  }, []);
}

function countUniqueChars(line) {
  const charsInLine = new Set([...line]);
  return charsInLine.size;
}

parts.part1 = async function() {
  const lines = toEntryArray(await readInput());
  const reducedAnswers = reduceAnswers(lines);
  const countedUniqueLineItems = reducedAnswers.reduce((acc, line) => {
    acc += countUniqueChars(line);
    return acc;
  }, 0);

  return countedUniqueLineItems;
}

function reduceAnswers2(lines) {
  return lines.reduce((acc, line) => {
    if (acc.length === 0 || line === '') {
      acc.push({
        groupSize: acc.length === 0 ? 1 : 0,
        chars: line,
      });
    } else {
      const group = acc[acc.length - 1];
      group.groupSize +=1;
      group.chars += line;

      acc[acc.length - 1] = group;
    }

    return acc;
  }, []);
}

function countUniqueChars2(group) {
  const charsInLine = new Map();
  for (const char of group.chars) {
    if (charsInLine.has(char)) {
      charsInLine.set(char, charsInLine.get(char) + 1);
    } else {
      charsInLine.set(char, 1);
    }
  }

  group.uniques = [...charsInLine.values()].filter(v => v === group.groupSize).length;

  return group.uniques;
}

parts.part2 = async function() {
  const lines = toEntryArray(await readInput());
  const reducedAnswers = reduceAnswers2(lines);
  const countedUniqueLineItems = reducedAnswers.reduce((acc, group) => {
    acc += countUniqueChars2(group);
    return acc;
  }, 0);

  return countedUniqueLineItems;
}
