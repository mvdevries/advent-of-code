'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const Node = require('./node');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input.split('').map(v => new Node(Number(v)));
}

function addExtraCups(cups, maxAmount) {
  const cupValues = cups.map(c => c.value);
  for (let i = Math.max(...cupValues) + 1; i <= maxAmount; i++) {
    cups.push(new Node(Number(i)));
  }
}

function linkCups(cups) {
  for (const [index, cup] of cups.entries()) {
    cup.next = index < cups.length - 1 ? cups[index + 1] : cups[0];
  }
}

function toCupMap(cups) {
  return new Map(cups.map(cup => [cup.value, cup]));
}

function getDestinationCup(cups, cupsValueMap, currentCup, pickupValues) {
  let searchValue = currentCup.value - 1;
  if (searchValue === 0) {
    searchValue = cupsValueMap.size;
  }

  while (pickupValues.includes(searchValue)) {
    searchValue--;
    if (searchValue === 0) {
      searchValue = cupsValueMap.size;
    }
  }

  return cupsValueMap.get(searchValue);
}

function round(cups, cupsValueMap, currentCup, round) {
  const pickupStart = currentCup.next;
  const pickupValues = [
    currentCup.next.value,
    currentCup.next.next.value,
    currentCup.next.next.next.value,
  ];

  currentCup.next = currentCup.next.next.next.next;

  const destination = getDestinationCup(cups, cupsValueMap, currentCup, pickupValues);

  pickupStart.next.next.next = destination.next;
  destination.next = pickupStart;
}

function toCupsArray(cupsValueMap, fromValue) {
  const valuesArray = [];
  let currentCup = cupsValueMap.get(fromValue);

  let count = 1;
  let max = cupsValueMap.size;

  let done = false;
  while (!done) {
    valuesArray.push(currentCup.value);
    currentCup = currentCup.next;
    count++;

    if (count > max) {
      done = true;
    }
  }

  return valuesArray;
}

function game(cups, cupsValueMap, rounds) {
  let currentCup = cups[0];
  for (let i = 0; i < rounds; i++) {
    round(cups, cupsValueMap, currentCup, i);
    currentCup = currentCup.next;
  }
}

parts.part1 = async function() {
  const cups = parseInput(await readInput());
  linkCups(cups);
  const cupsValueMap = toCupMap(cups);
  game(cups, cupsValueMap, 100);  game(cups, cupsValueMap, 100)

  return toCupsArray(cupsValueMap, 1).join('').replace('1', '');
};

parts.part2 = async function() {
  const cups = parseInput(await readInput());
  addExtraCups(cups, 1_000_000)
  linkCups(cups);
  const cupsValueMap = toCupMap(cups);
  game(cups, cupsValueMap, 10_000_000);

  const cup1 = cupsValueMap.get(1);
  return cup1.next.value * cup1.next.next.value;
};
