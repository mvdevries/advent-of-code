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

function parseMinMax(minMax) {
  const [min, max] = minMax.split('-');
  return {
    min,
    max
  };
}

function parseChar(char) {
  return char.replace(':', '');
}

function parsePasswordLine(passwordLine) {
  return passwordLine.split(' ');
}

function countOccurrences(password, char) {
  let counter = 0;
  for (const passwordChar of password) {
    if (passwordChar === char) {
      counter++;
    }
  }
  return counter;
}

function isValidPassword(occurrences, min, max) {
  return occurrences >= min && occurrences <= max;
}

parts.part1 = async function() {
  const lines = toEntryArray(await readInput())
  const validPasswords = lines
    .map(pl => {
      const [minMax, rawChar, password] = parsePasswordLine(pl);
      const {min, max} = parseMinMax(minMax)
      const char = parseChar(rawChar)
      const occurrences = countOccurrences(password, char);
      return isValidPassword(occurrences, min, max);
    })
    .filter(v => v);

  return validPasswords.length;
}

function hasChar(password, char, pos) {
  return password.charAt(pos - 1) === char;
}

function parsePositions(positions) {
  return positions.split('-');
}

parts.part2 = async function() {
  const validPasswords = toEntryArray(await readInput())
    .map(pl => {
      const [positions, rawChar, password] = parsePasswordLine(pl);
      const [pos1, pos2] = parsePositions(positions)
      const char = parseChar(rawChar)
      return hasChar(password, char, parseInt(pos1)) ^ hasChar(password, char, parseInt(pos2));
    })
    .filter(v => v);

  return validPasswords.length;
}
