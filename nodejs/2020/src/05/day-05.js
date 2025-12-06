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

function getRowOrColumn(line, start, end, lowerLetter, higherLetter) {
  for (const char of line) {
    if (char === lowerLetter) {
      end = Math.floor(end - ((end - start) / 2));
    } else if (char === higherLetter) {
      start = Math.ceil(start + ((end - start) / 2));
    }
  }

  if (start !== end) {
    console.log('start and end are not equal', start, end, line);
  }

  const lastLetter = line.charAt(5);
  if (lastLetter === lowerLetter) {
    return start;
  } else {
    return end;
  }
}

function checkBoardingPass(lines) {
  return lines.map(line => {
    const row = getRowOrColumn(line.substring(0, 7), 0, 127, 'F', 'B');
    const column = getRowOrColumn(line.substring(7, 10), 0, 7, 'L', 'R');
    return new Seat(row, column);
  });
}

parts.part1 = async function() {
  const lines = toEntryArray(await readInput());

  const seats = checkBoardingPass(lines)

  let seatId = 0;
  for (const seat of seats) {
    if (seat.getId() > seatId) {
      seatId = seat.getId();
    }
  }

  return seatId;
}

parts.part2 = async function() {
  const lines = toEntryArray(await readInput());

  const seats = checkBoardingPass(lines)

  const sortedSeatIds = seats.map(s => s.getId()).sort((a, b) => a - b);
  const possibleMissingSeatIds = [];

  let previousId;
  for (const seatId of sortedSeatIds) {
    if (!!previousId && seatId - 1 !== previousId) {
      possibleMissingSeatIds.push(seatId - 1);
    }

    previousId = seatId;
  }

  return possibleMissingSeatIds[0];
}

class Seat {
  constructor(row, column) {
    this._row = row;
    this._column = column;
  }

  getId() {
    return this._row * 8 + this._column;
  }
}
