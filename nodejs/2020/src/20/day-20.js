'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const {SuperTile, Tile} = require('./tile');
const {edges} = require('./constants');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input.split('\n\n').map(rawTile => new Tile(rawTile));
}

function getCornerCandidates(tiles) {
  const cornerCandidates = [];
  for (const tile of tiles) {
    for (const comparisonTile of tiles) {
      if (tile.id === comparisonTile.id) {
        continue;
      }

      for (const edge of tile.originalEdgesAndReverse) {
        if (comparisonTile.originalEdgesAndReverse.includes(edge)) {
          tile.edgesMatched++;
        }
      }
    }

    if (tile.edgesMatched === 4) {
      cornerCandidates.push(tile.id);
    }
  }

  return cornerCandidates;
}

parts.part1 = async function() {
  const tiles = parseInput(await readInput());
  const cornerCandidates = getCornerCandidates(tiles);
  return cornerCandidates.reduce((a, b) => a * b, 1);
};

function getTileIdMap(tiles) {
  const tileIdMap = new Map();
  tiles.map(tile => tileIdMap.set(tile.id, tile));
  return tileIdMap;
}

function getAllTilePositions(tiles, cornerCandidates) {
  const width = Math.sqrt(tiles.length);
  const candidates = getTileIdMap(tiles);
  const positions = new Map();

  // Place 1st tile at position 0-0
  const startingCorner = candidates.get(cornerCandidates[0]);
  candidates.delete(startingCorner.id);
  startingCorner.orientStart(candidates);
  positions.set('0-0', startingCorner);

  for (let y = 0; y < width; y++) {
    for (let x = 0; x < width; x++) {
      if (positions.has(`${y}-${x}`)) {
        continue;
      }

      const knownNeighborsTiles = {
        north: positions.get(`${y - 1}-${x}`),
        east: positions.get(`${y}-${x + 1}`),
        south: positions.get(`${y + 1}-${x}`),
        west: positions.get(`${y}-${x - 1}`),
      };

      for (const [id, candidateTile] of candidates) {
        const canMatch = candidateTile.match(knownNeighborsTiles);
        if (canMatch) {
          candidates.delete(id);
          positions.set(`${y}-${x}`, candidateTile);
        }
      }
    }
  }

  return positions;
}


parts.part2 = async function() {
  const tiles = parseInput(await readInput());
  const cornerCandidates = getCornerCandidates(tiles);
  const positions = getAllTilePositions(tiles, cornerCandidates);

  const superTile = new SuperTile(positions);
  const numMonsters = superTile.findMonsters();
  return superTile.countClearOfMonster();
};
