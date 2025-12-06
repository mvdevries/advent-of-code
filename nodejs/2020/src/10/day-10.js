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

function getJoltDiffs(jolts) {
  return jolts.reduce((acc, jolt) => {
    const diff = jolt - acc.current;
    acc[`jolt${diff}`] += 1;
    acc.current = jolt;
    return acc;
  }, { current: 0, jolt1: 0, jolt2: 0, jolt3: 1 });
}

parts.part1 = async function() {
  const jolts = toEntryArray(await readInput());
  jolts.sort((a, b) => a - b);
  const answers = getJoltDiffs(jolts);
  return answers.jolt1 * answers.jolt3;
};

function getAllowedArrangementCount(jolts) {
  return jolts.reduce((acc, jolt) => {
    const jolt3 = acc.get(jolt - 3) || 0;
    const jolt2 = acc.get(jolt - 2) || 0;
    const jolt1 = acc.get(jolt - 1) || 0;
    acc.set(jolt, jolt3 + jolt2 + jolt1);
    return acc;
  }, new Map([[0, 1]]));
}

parts.part2 = async function() {
  const jolts = toEntryArray(await readInput());
  jolts.sort((a, b) => a - b);

  const countMap = getAllowedArrangementCount(jolts);
  return [...countMap.values()].pop();
};
