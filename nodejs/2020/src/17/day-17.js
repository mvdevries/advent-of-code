'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toCubeArray(input) {
  return input.split('\n').filter(n => n).map(n => [...n]);
}

function createActiveCubeSetFromStartArray(startArray) {
  const activeCubeSet = new Set();

  for (let y = 0; y < startArray.length; y++) {
    for (let x = 0; x < startArray[0].length; x++) {
      if (startArray[y][x] === '#') {
        activeCubeSet.add(`${x},${y},0,0`);
      }
    }
  }

  return activeCubeSet;
}

function createDimensionsFromStartArray(startArray) {
  return {
    wAxis: { min: 0, max: 1 },
    zAxis: { min: 0, max: 1 },
    yAxis: { min: 0, max: startArray.length },
    xAxis: { min: 0, max: startArray[0].length },
  };
}

function updateDimensions(dimensions, hasAxisW) {
  if (hasAxisW) {
    dimensions.wAxis.min--;
    dimensions.wAxis.max++;
  }

  dimensions.zAxis.min--;
  dimensions.zAxis.max++;
  dimensions.yAxis.min--;
  dimensions.yAxis.max++;
  dimensions.xAxis.min--;
  dimensions.xAxis.max++;
}

function countActiveNeighbors(activeCubeSet, w, z, y, x, hasAxisW) {
  let count = 0;
  for (let wd = (hasAxisW ? w - 1 : 0); wd <= (hasAxisW ? w + 1 : 0); wd++) {
    for (let zd = z - 1; zd <= z + 1; zd++) {
      for (let yd = y - 1; yd <= y + 1; yd++) {
        for (let xd = x - 1; xd <= x + 1; xd++) {
          if ((wd !== w || zd !== z || yd !== y || xd !== x ) && activeCubeSet.has(`${xd},${yd},${zd},${wd}`)) {
            count++;
          }
        }
      }
    }
  }
  return count;
}

function round(activeCubeSet, d, hasAxisW = false) {
  const newActiveCubeSet = new Set();
  updateDimensions(d, hasAxisW);

  for (let w = d.wAxis.min; w < d.wAxis.max; w++) {
    for (let z = d.zAxis.min; z < d.zAxis.max; z++) {
      for (let y = d.yAxis.min; y < d.yAxis.max; y++) {
        for (let x = d.xAxis.min; x < d.xAxis.max; x++) {
          let count = countActiveNeighbors(activeCubeSet, w, z, y, x, hasAxisW);
          const isActive = activeCubeSet.has(`${x},${y},${z},${w}`);
          if (count === 3 || (count === 2 && isActive)) {
            newActiveCubeSet.add(`${x},${y},${z},${w}`);
          }
        }
      }
    }
  }

  return newActiveCubeSet;
}

parts.part1 = async function() {
  console.time('part 1');
  const input = await readInput();
  const startArray = toCubeArray(input);
  let activeCubeSet = createActiveCubeSetFromStartArray(startArray);
  const dimensions = createDimensionsFromStartArray(startArray);

  for (let r = 0; r < 6; r++) {
    activeCubeSet = round(activeCubeSet, dimensions);
  }

  console.timeEnd('part 1');
  return activeCubeSet.size;
};

parts.part2 = async function() {
  console.time('part 2');
  const input = await readInput();
  const startArray = toCubeArray(input);
  let activeCubeSet = createActiveCubeSetFromStartArray(startArray);
  const dimensions = createDimensionsFromStartArray(startArray);

  for (let r = 0; r < 6; r++) {
    activeCubeSet = round(activeCubeSet, dimensions, true);
  }

  console.timeEnd('part 2');
  return activeCubeSet.size;
};

(async () => {
  console.time('parts');
  await parts.part1();
  await parts.part2();
  console.timeEnd('parts');
})();
