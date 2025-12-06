'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseTileDirections(input) {
  return input
    .split('\n')
    .filter(n => n)
    .map(row => {
      let letters = row.split('');
      const directions = [];
      while (letters.length) {
        let letter = letters.shift();
        if (/n|s/.test(letter)) {
          letter = letter + letters.shift();
        }
        directions.push(letter);
      }

      return directions;
    });
}

function calculateDirection(direction) {
  switch(direction){
    case 'nw':
      return [-.5, -1];
    case 'ne':
      return [.5, -1];
    case 'w':
      return [-1, 0];
    case 'e':
      return [1, 0];
    case 'sw':
      return [-.5, 1];
    case 'se':
      return [.5, 1];
  }
}

function getBlackTilesFromDirectionInstructions(tileDirectionInstructions) {
  const blackTiles = new Set();

  for (const tileDirections of tileDirectionInstructions) {
    let [x, y] = [0, 0];

    for (const tileDirection of tileDirections) {
      const [dx, dy] = calculateDirection(tileDirection);
      x += dx;
      y += dy;
    }

    const location = `${x}-${y}`;
    if (blackTiles.has(location)) {
      blackTiles.delete(location)
    } else {
      blackTiles.add(location);
    }
  }
}

parts.part1 = async function() {
  const tileDirectionInstructions = parseTileDirections(await readInput());
  const blackTiles = getBlackTilesFromDirectionInstructions(tileDirectionInstructions);
  return blackTiles.size;
};

parts.part2 = async function() {
  const tileDirections = parseInput(await readInput());
  const blackTiles = getBlackTilesFromDirections(tileDirections);

};

(async() => {
  console.log(await parts.part2());
})();

class Grid {
  constructor() {
    this.tiles = this.getNewGrid(201);
    this.nextDayTiles = this.getNewGrid(201);
  }

  getNewGrid(size) {
    const grid = [];
    for (let i = 0; i < size; i++) {
      grid.push(Array(size).fill('w'));
    }
    return grid;
  }

  applyAll(tileDirections) {
    for (const tileDirectionInstructions of tileDirections) {
      this.flipOne(tileDirectionInstructions);
    }
  }

  flipOne(tileDirectionInstructions) {
    let i, j;
    i = j = 100;

    for (const letter of instruction) {
      switch (letter) {
        case 'w':
          j--;
          break;
        case 'R':
          i--;
          break;
        case 'S':
          i--;
          j++;
          break;
        case 'e':
          j++;
          break;
        case 'T':
          i++;
          break;
        case 'U':
          i++;
          j--;
          break;
      }
    }

    if (this.tiles[i][j] === 'w') {
      this.tiles[i][j] = 'b';
    } else {
      this.tiles[i][j] = 'w';
    }
  }
}
