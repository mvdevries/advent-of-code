'use strict';

const edges = {
  north: 'north',
  east: 'east',
  south: 'south',
  west: 'west',
};

const tileHeightWidth = 10;

const monster = [
  '                  # '.split(''),
  '#    ##    ##    ###'.split(''),
  ' #  #  #  #  #  #   '.split(''),
];
const monsterWidth = monster[0].length;
const monsterHeight = monster.length;

const monsterCoordinates = [];
for (let y = 0; y < monsterHeight; y++) {
  for (let x = 0; x < monsterWidth; x++) {
    if (monster[y][x] === '#') monsterCoordinates.push([y, x]);
  }
}

module.exports = {
  edges,
  tileHeightWidth,
  monster,
  monsterWidth,
  monsterHeight,
  monsterCoordinates,
};
