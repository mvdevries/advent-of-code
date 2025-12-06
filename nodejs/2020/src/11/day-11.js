'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

const seatTypes = {
  empty: 'L',
  occupied: '#',
  no: '.'
};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split('\n').filter(n => n).map(n => [...n]);
}

const deltas = [
  {x: 0, y: -1},
  {x: 1, y: -1},
  {x: 1, y: 0},
  {x: 1, y: 1},
  {x: 0, y: 1},
  {x: -1, y: 1},
  {x: -1, y: 0},
  {x: -1, y: -1},
];

function searchInDirection(floor, x, y, dx, dy, seatType) {
  do {
    y += dy;
    x += dx;
  }
  while (!!floor[y] && !!floor[y][x] && floor[y][x] === seatTypes.no);

  return !!floor[y] && !!floor[y][x] && floor[y][x] === seatType;
}

function checkInSightAdjacent(floor, x, y, seatType) {
  return deltas.reduce((seats, delta) => {
    if (searchInDirection(floor, x, y, delta.x, delta.y, seatType)) {
      seats +=1;
    }
    return seats;
  }, 0);
}

function checkDirectAdjacent(floor, x, y, seatType) {
  return deltas.reduce((seats, delta) => {
    if (!!floor[y + delta.y] && !!floor[y + delta.y][x + delta.x] && floor[y + delta.y][x + delta.x] === seatType) {
      seats +=1;
    }
    return seats;
  }, 0);
}

function round(oldFloor, checkAdjacentFunction, allowence) {
  return oldFloor.map((row, y) => {
    return row.map((seat, x) => {
      const adjacentOccupiedSeatsCount = checkAdjacentFunction(oldFloor, x, y, seatTypes.occupied)
      if (seat === seatTypes.empty && adjacentOccupiedSeatsCount === 0) {
        return seatTypes.occupied;
      } else if (seat === seatTypes.occupied && adjacentOccupiedSeatsCount >= allowence) {
        return seatTypes.empty;
      }

      return seat;
    });
  });
}

function floorsAreEqual(floor1, floor2) {
  if (floor1.length !== floor2.length) {
    return false;
  }

  return JSON.stringify(floor1) === JSON.stringify(floor2);
}

function countSeats(floor, seatType) {
  return floor.reduce((acc, r) => {
    acc += r.filter(s => s === seatType).length;
    return acc;
  }, 0);
}

parts.part1 = async function() {
  let oldFloor = [];
  let newFloor = toEntryArray(await readInput());
  while (!floorsAreEqual(oldFloor, newFloor)) {
    oldFloor = newFloor;
    newFloor = round(oldFloor, checkDirectAdjacent, 4);
  }

  return countSeats(newFloor, seatTypes.occupied);
};

parts.part2 = async function() {
  let oldFloor = [];
  let newFloor = toEntryArray(await readInput());
  while (!floorsAreEqual(oldFloor, newFloor)) {
    oldFloor = newFloor;
    newFloor = round(oldFloor, checkInSightAdjacent, 5);
  }

  return countSeats(newFloor, '#');
};
