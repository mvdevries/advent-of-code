'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split(',').filter(n => n).map(n => parseInt(n, 10));
}

parts.part1 = async function() {
  const input = toEntryArray(await readInput());

  const memory = Array.from(input);

  for (let i = input.length; i < 2020; i++) {
    const lastNumber = memory[memory.length - 1];
    const lastNumberIndex = memory.lastIndexOf(lastNumber, memory.length - 1);
    const lastNumberLastIndex = memory.lastIndexOf(lastNumber, lastNumberIndex - 1);

    if (lastNumberLastIndex >= 0) {
      // number has been seen before
      memory.push(lastNumberIndex - lastNumberLastIndex);

    } else {
      // number has never been seen before
      memory.push(0);
    }
  }

  return memory[memory.length - 1];
};

parts.part2 = async function() {
  const input = toEntryArray(await readInput());

  const memory = new Map();
  input.forEach((n, i) => memory.set(n, i));

  let lastNumber = input[input.length - 1];
  let lastNumberIndex = input.length - 1;
  let lastNumberLastIndex = undefined;

  for (let i = input.length; i < 30000000; i++) {
    let thisNumber;
    if (lastNumberLastIndex !== undefined) {
      // number has been seen before
      thisNumber = lastNumberIndex - lastNumberLastIndex;

    } else {
      // number has never been seen before
      thisNumber = 0;

    }

    lastNumber = thisNumber;
    lastNumberIndex = i;
    lastNumberLastIndex = memory.get(thisNumber);

    memory.set(thisNumber, i);
  }

  return lastNumber;
};
