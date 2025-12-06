'use strict';
const {
  edges,
  tileHeightWidth,
  monsterWidth,
  monsterHeight,
  monsterCoordinates,
} = require('./constants');

class BaseTile {
  constructor() {
    this._currentGrid = [[]];
    this.currentEdges = new Map();
  }

  get currentWidthAndHeight() {
    return this.currentGrid.length;
  }

  get currentGrid() {
    return this._currentGrid;
  }

  set currentGrid(value) {
    this._currentGrid = value;
  }

  *getNextPosition() {
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    this.flipVertically();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    this.flipVertically();
    this.rotate90clockwise();
    this.flipVertically();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    yield true;
    this.rotate90clockwise();
    yield true;
    yield false;
  }

  flipVertically() {
    for (let i = 0; i < this.currentWidthAndHeight; i++) {
      this.currentGrid[i].reverse();
    }
    this.refreshCurrentEdges();
  }

  rotate90clockwise() {
    const x = this.currentWidthAndHeight / 2;
    const y = this.currentWidthAndHeight - 1;
    for (let i = 0; i < x; i++) {
      for (let j = i; j < y - i; j++) {
        const k = this.currentGrid[i][j];
        this.currentGrid[i][j] = this.currentGrid[y - j][i];
        this.currentGrid[y - j][i] = this.currentGrid[y - i][y - j];
        this.currentGrid[y - i][y - j] = this.currentGrid[j][y - i];
        this.currentGrid[j][y - i] = k;
      }
    }
    this.refreshCurrentEdges();
  }

  refreshCurrentEdges() {
    this.currentEdges.set(edges.north, this.getEdge(this.currentGrid, edges.north).join(''));
    this.currentEdges.set(edges.east, this.getEdge(this.currentGrid, edges.east).join(''));
    this.currentEdges.set(edges.south, this.getEdge(this.currentGrid, edges.south).join(''));
    this.currentEdges.set(edges.west, this.getEdge(this.currentGrid, edges.west).join(''));
  }

  getEdgesAndReverse(grid) {
    return [
      this.getEdge(grid, edges.north),
      this.getReverseEdge(grid, edges.north),
      this.getEdge(grid, edges.east),
      this.getReverseEdge(grid, edges.east),
      this.getEdge(grid, edges.south),
      this.getReverseEdge(grid, edges.south),
      this.getEdge(grid, edges.west),
      this.getReverseEdge(grid, edges.west),
    ];
  }

  getEdge(grid, edge) {
    let yStart;
    let yEnd;
    let xStart;
    let xEnd;
    if (edge === edges.north) {
      yStart = 0;
      yEnd = 1;
      xStart = 0;
      xEnd = 10;
    } else if (edge === edges.east) {
      yStart = 0;
      yEnd = 10;
      xStart = 9;
      xEnd = 10;
    } else if (edge === edges.south) {
      yStart = 9;
      yEnd = 10;
      xStart = 0;
      xEnd = 10;
    } else if (edge === edges.west) {
      yStart = 0;
      yEnd = 10;
      xStart = 0;
      xEnd = 1;
    }

    const edgeArr = [];
    for (let y = yStart; y < yEnd; y++) {
      for (let x = xStart; x < xEnd; x++) {
        if (grid[y][x]) {
          edgeArr.push(grid[y][x]);
        }
      }
    }

    return edgeArr;
  }

  getReverseEdge(grid, edge) {
    return this.getEdge(grid, edge).reverse();
  }
}

class Tile extends BaseTile {
  constructor(tileBlob) {
    super();
    const [title, rawGrid] = tileBlob.split(':\n');
    this.id = Number(title.slice(5, 10));
    this.originalGrid = this.layGrid(rawGrid);
    this.originalEdgesAndReverse = this.getEdgesAndReverse(this.originalGrid).map(edge => edge.join(''));
    this.edgesMatched = 0;

    this.currentGrid = this.originalGrid.map(line => [...line]);
    this.refreshCurrentEdges();
  }

  layGrid(rawGrid) {
    return rawGrid.split('\n').map(line => line.split(''));
  }

  hasRightAndBottomMatch(tileMap) {
    return this.hasMatch(tileMap, edges.east) && this.hasMatch(tileMap, edges.south);
  }

  matchesTiles(otherTile, edgeType) {
    switch (edgeType) {
      case edges.north:
        return (otherTile.currentEdges.get(edges.south) === this.currentEdges.get(edges.north));
      case edges.east:
        return (otherTile.currentEdges.get(edges.west) === this.currentEdges.get(edges.east));
      case edges.south:
        return (otherTile.currentEdges.get(edges.north) === this.currentEdges.get(edges.south));
      case edges.west:
        return (otherTile.currentEdges.get(edges.east) === this.currentEdges.get(edges.west));
      default:
        throw Error('no Match possible?');
    }
  }

  hasMatch(tileMap, edgeType) {
    for (const [key, tile] of tileMap) {
      if (tile.originalEdgesAndReverse.includes(this.currentEdges.get(edgeType))) {
        return true;
      }
    }
    return false;
  }

  orientStart(candidates) {
    while (true) {
      this.rotate90clockwise();
      if (this.hasRightAndBottomMatch(candidates)) {
        return;
      }
    }
  }

  match(knownNeighborTiles) {
    const positionIterator = this.getNextPosition();
    while (positionIterator.next().value) {
      let abort = false;
      for (const edgeType in knownNeighborTiles) {
        if (knownNeighborTiles[edgeType]) {
          if (!this.matchesTiles(knownNeighborTiles[edgeType], edgeType)) {
            abort = true;
            break;
          }
        }
      }

      if (abort) {
        continue;
      } else {
        return true;
      }
    }

    return false;
  }
}

class SuperTile extends BaseTile {
  constructor(positions) {
    super();
    const width = Math.sqrt(positions.size);
    this.width = width * 8;
    this.currentGrid = new Array(width * (tileHeightWidth - 2));
    for (let y = 0; y < this.currentGrid.length; y++) {
      this.currentGrid[y] = new Array(width * (tileHeightWidth - 2));
    }

    for (let y = 0; y < width; y++) {
      for (let x = 0; x < width; x++) {
        const currentTile = positions.get(`${y}-${x}`);
        for (let ty = 1; ty < tileHeightWidth - 1; ty++) {
          for (let tx = 1; tx < tileHeightWidth - 1; tx++) {
            this.currentGrid[y * (tileHeightWidth - 2) + ty - 1][x * (tileHeightWidth - 2) + tx - 1] =
              currentTile.currentGrid[ty][tx];
          }
        }
      }
    }
  }

  findMonsters() {
    let numMonsters = 0;
    const positionIterator = this.getNextPosition();
    while (numMonsters === 0 && positionIterator.next().value) {
      for (let y = 0; y <= this.width - monsterHeight; y++) {
        for (let x = 0; x <= this.width - monsterWidth; x++) {
          if (!!monsterCoordinates.every(([my, mx]) => this.currentGrid[y + my][x + mx] === '#')) {
            numMonsters++;
            monsterCoordinates.map(([my, mx]) => (this.currentGrid[y + my][x + mx] = '0'));
          }
        }
      }
    }
    return numMonsters;
  }

  countClearOfMonster() {
    let num = 0;
    for (let y = 0; y < this.width; y++) {
      for (let x = 0; x < this.width; x++) {
        if (this.currentGrid[y][x] === '#') {
          num++;
        }
      }
    }
    return num;
  }
}

module.exports = {
  Tile,
  SuperTile,
};
