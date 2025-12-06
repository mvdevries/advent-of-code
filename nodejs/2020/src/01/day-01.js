'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split('\n').filter(n => n).map(n => parseInt(n));
}

function calculate1(entries) {
  const entrySet = new Set()

  for (const entry of entries) {
    const remainder = 2020 - entry;
    if (entrySet.has(remainder)) {
      return entry * remainder;
    }
    entrySet.add(entry);
  }
}

function calculate2(entries) {
  const entrySet = new Set(entries)

  for (let i = 0; i < entries.length; i++) {
    const entry1 = entries[i];
    const remainder23 = 2020 - entry1;

    for (let j = i + 1; j < entries.length; j++) {
      const entry2 = entries[j];
      const remainder3 = remainder23 - entry2;

      if (entrySet.has(remainder3)) {
        return entry1 * entry2 * remainder3
      }
    }
  }
}

parts.part1 = async function() {
  const entries = toEntryArray(await readInput());
  return calculate1(entries);
}

parts.part2 = async function() {
  const entries = toEntryArray(await readInput());
  entries.sort();

  return calculate2(entries);
}
