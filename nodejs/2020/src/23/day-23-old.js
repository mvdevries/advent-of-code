'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input.split('').map(Number);
}

function getDestinationIndex(cups, cupsCount, currentCup) {
  let nextCup = currentCup;

  do {
    nextCup--;
    if (nextCup === 0) {
      nextCup += cupsCount;
    }
  }
  while (cups.indexOf(nextCup) === -1);
  return cups.indexOf(nextCup) + 1;
}

function round(cups, round, cupsCount) {
  const pickUpCups = cups.splice(1, 3);
  const destinationIndex = getDestinationIndex(cups, cupsCount, cups[0]);
  cups.splice(destinationIndex, 0, ...pickUpCups);
  cups.push(cups.shift());
}

function game(cups, rounds) {
  for (let i = 0; i < rounds; i++) {
    // if (i % 1000 === 0) {
    //   process.stdout.write("  " + (i / (rounds / 100)).toFixed(2) + "%\r");
    // }
    console.time(`Round ${i + 1}`);
    round(cups, i, cups.length);
    console.timeEnd(`Round ${i + 1}`);
  }
}

parts.part1 = async function() {
  const cups = parseInput(await readInput());
  game(cups, 100);

  cups.push(...cups.splice(0, cups.indexOf(1)));
  return cups.join('');
};

function putInExtraCups(cups) {
  for (let i = Math.max(...cups) + 1; i <= 1000000; i++) {
    cups.push(i);
  }
}

parts.part2 = async function() {
  const cups = parseInput(await readInput());
  putInExtraCups(cups)
  game(cups, 10000000);
  const indexOf1 = cups.indexOf(1);
  return cups[indexOf1 + 1] * cups[indexOf1 + 2];
};
