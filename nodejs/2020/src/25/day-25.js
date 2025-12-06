'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input.split('\n').filter(n => n).map(Number);
}

parts.part1 = async function() {
  const [cardSubjectNumber, doorSubjectNumber] = parseInput(await readInput());

  let key = 1;
  let target = 1;
  while (target !== doorSubjectNumber) {
    target = (target * 7) % magicNumber;
    key = (key * cardSubjectNumber) % magicNumber;
  }
  return key;
};
