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

parts.part1 = async function() {
  const lines = toEntryArray(await readInput());

  let trees = 0;
  let rightPos = 0;

  for (const line of lines) {
    if (line.charAt(rightPos) === '#') {
      trees += 1;
    }

    rightPos += 3;
    rightPos %= 31;
  }

  return trees;
}

function countTreesOnSlope(lines, right, down) {
  let rightPos = 0;
  let trees = 0;

  for (let i = 0; i < lines.length; i+=down) {
    const line = lines[i];
    if (line.charAt(rightPos) === '#') {
      trees += 1;
    }

    rightPos += right;
    rightPos %= 31;
  }

  return trees;
}

parts.part2 = async function() {
  const lines = toEntryArray(await readInput());

  let count = countTreesOnSlope(lines, 1, 1);
  count *= countTreesOnSlope(lines, 3, 1);
  count *= countTreesOnSlope(lines, 5, 1);
  count *= countTreesOnSlope(lines, 7, 1);
  count *= countTreesOnSlope(lines, 1, 2);

  return count;
}
