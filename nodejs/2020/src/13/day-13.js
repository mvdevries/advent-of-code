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

function toBusLines(busLines) {
  return busLines.split(',').filter(l => l !== 'x').map(l => parseInt(l, 10));
}

function toBusLinesWithIndex(busLines) {
  return busLines
    .split(',')
    .map((n, index) => {
      return {
        busLine: parseInt(n, 10),
        index,
      };
    })
    .filter(({busLine}) => !Number.isNaN(busLine));
}

parts.part1 = async function() {
  const input = await readInput();
  const [nowRaw, busses] = toEntryArray(input);
  const busLines = toBusLines(busses);
  const now = parseInt(nowRaw, 10);

  const earliestBusAndTime = busLines
    .reduce((currentBusLine, busLine) => {
      const tillArrival = busLine - (now % busLine);
      if (tillArrival < currentBusLine.tillArrival) {
        currentBusLine.tillArrival = tillArrival;
        currentBusLine.busLine = busLine;
      }
      return currentBusLine
    }, {tillArrival: now, busLine: 0});

  return earliestBusAndTime.tillArrival * earliestBusAndTime.busLine
};

parts.part2 = async function() {
  const input = await readInput();
  const lines = toEntryArray(input);
  const [firstBus, ...buses] = toBusLinesWithIndex(lines[1]);

  let multiplier = firstBus.busLine;
  let i = firstBus.index;

  buses.forEach(({busLine, index}) => {
    while (true) {
      if ((i + index) % busLine === 0) {
        multiplier *= busLine;
        break;
      }
      i += multiplier;
    }
  });

  return i;
};
