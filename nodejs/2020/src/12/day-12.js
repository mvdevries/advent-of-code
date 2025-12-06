'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

const direction = {
  north: 0,
  east: 90,
  south: 180,
  west: 270
};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split('\n').filter(n => n).map(line => {
    const [action, ...units] = line;
    return {
      action,
      amount: parseInt(units.join(''), 10),
    };
  });
}


parts.part1 = async function() {
  const actions = toEntryArray(await readInput());
  let eastWest = 0;
  let northSouth = 0;
  let dir = direction.east;

  function adjustDir(amount) {
    dir += amount;

    while (dir > 360) {
      dir -= 360;
    }

    while (dir < 0) {
      dir += 360;
    }

    if (dir === 360) {
      dir = 0;
    }
  }

  for (const step of actions) {
    switch (step.action) {
      case 'N':
        northSouth += step.amount;
        break;
      case 'S':
        northSouth -= step.amount;
        break;
      case 'E':
        eastWest += step.amount;
        break;
      case 'W':
        eastWest -= step.amount;
        break;
      case 'L':
        adjustDir(-1 * step.amount);
        break;
      case 'R':
        adjustDir(step.amount);
        break;
      case 'F':
        switch (dir) {
          case direction.north:
            northSouth += step.amount;
            break;
          case direction.south:
            northSouth -= step.amount;
            break;
          case direction.east:
            eastWest += step.amount;
            break;
          case direction.west:
            eastWest -= step.amount;
            break;
          default:
            throw new Error(`Unknown direction: ${ dir }`);
        }
        break;
      default:
        throw new Error('Invalid step');
    }
  }

  return Math.abs(eastWest) + Math.abs(northSouth);
};

parts.part2 = async function() {
  const actions = toEntryArray(await readInput());
  let shipX = 0;
  let shipY = 0;
  let wayX = 10;
  let wayY = 1;

  function rotateWaypoint(amount) {
    while (amount > 360) {
      amount -= 360;
    }

    while (amount < 0) {
      amount += 360;
    }

    switch (amount) {
      case 0:
      case 360:
        return;
      case 90: {
        const tmpY = wayY;
        wayY = -1 * wayX;
        wayX = tmpY;
        break;
      }
      case 180: {
        wayY = -1 * wayY;
        wayX = -1 * wayX;
        break;
      }
      case 270: {
        const tmpY = wayY;
        wayY = wayX;
        wayX = -1 * tmpY;
        break;
      }
      default:
        throw new Error(`Unknown rotation: ${ amount }`);
    }
  }

  for (const step of actions) {
    switch (step.action) {
      case 'N':
        wayY += step.amount;
        break;
      case 'S':
        wayY -= step.amount;
        break;
      case 'E':
        wayX += step.amount;
        break;
      case 'W':
        wayX -= step.amount;
        break;
      case 'L':
        rotateWaypoint(-1 * step.amount);
        break;
      case 'R':
        rotateWaypoint(step.amount);
        break;
      case 'F':
        shipX += (wayX * step.amount);
        shipY += (wayY * step.amount);
        break;
      default:
        throw new Error('Invalid step');
    }
  }

  return Math.abs(shipX) + Math.abs(shipY);
};

(async () => {
  try {
    console.log(await parts.part2());
  } catch(err) {
    console.log(err);
  }
})();
