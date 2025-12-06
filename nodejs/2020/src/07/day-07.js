'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split('\n').filter(n => n);
}

function isNumeric(num){
  return !isNaN(num)
}

function parseInnerBags(words) {
  return words.reduce((acc, word) => {
    if (isNumeric(word)) {
      acc.push([word]);
    } else {
      acc[acc.length - 1].push(word);
    }

    if (acc[acc.length - 1].length === 4) {
      const amount = parseInt(acc[acc.length - 1].shift());
      acc[acc.length - 1] = parseRule(acc[acc.length - 1].join(' '), amount);
    }

    return acc;
  }, []);
}

function parseRule(line, amount) {
  const [type, color, ...words] = line.split(' ');
  const bag = new BagType(type, color, amount);

  words.splice(0, 2);
  if (words[0] !== 'no') {
    bag.addInnerBags(parseInnerBags(words));
  }

  return bag;
}

function findCanHold(bags, searchForBags) {
  const bagsCanHold = [];

  for (const bag of bags) {
    if (bag._innerBags.has(searchForBags)) {
      bagsCanHold.push(bag);
    }
  }

  for (const bag of bagsCanHold) {
    const canIndirectlyHold = findCanHold(bags, bag.id);
    for (const indirectBag of canIndirectlyHold) {
      bagsCanHold.push(indirectBag);
    }
  }

  return bagsCanHold;
}

parts.part1 = async function () {
  const lines = toEntryArray(await readInput());
  const bags = lines.map((rule) => parseRule(rule));
  const possibleBags = findCanHold(bags, 'shiny_gold');
  const uniquePossibleBags = new Map(possibleBags.map(pb => {
    return [
      pb.id,
      pb,
    ];
  }));

  return uniquePossibleBags.size;
}

function countInnerBags(bags, forId) {
  return bags
    .find(bag => bag.id === forId)
    .childs
    .reduce((acc, cb) => {
      return acc + cb._amount + (cb._amount * countInnerBags(bags, cb.id));
    }, 0);
}

parts.part2 = async function() {
  const lines = toEntryArray(await readInput());
  const bags = lines.map((rule) => parseRule(rule));

  return countInnerBags(bags, 'shiny_gold');
}

class BagType {
  constructor(type, color, amount) {
    this._type = type;
    this._color = color;
    this._amount = amount;
    this._innerBags = new Map();
  }

  get id() {
    return `${this._type}_${this._color}`
  }

  addInnerBags(bags) {
    for (const bag of bags) {
      this.addInnerBag(bag);
    }
  }

  addInnerBag(bag) {
    this._innerBags.set(bag.id, bag);
  }

  get childs() {
    return [...this._innerBags.values()];
  }
}
