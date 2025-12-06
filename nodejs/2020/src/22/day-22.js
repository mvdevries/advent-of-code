'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function gameIsFinished(player1, player2) {
  return player1.length === 0 || player2.length === 0;
}

function countScore(player) {
  return player.reverse().reduce((acc, n, i) => acc + n * (i + 1), 0);
}

function parseInput(input) {
  return input
    .split('\n\n')
    .map(playerData => {
      const [, ...cards] = playerData.split('\n');
      return cards.map(n => Number(n));
    });
}

function combatRound(player1, player2) {
  const card1 = player1.shift();
  const card2 = player2.shift();
  if (card1 > card2) {
    player1.push(card1, card2);
  } else if (card2 > card1) {
    player2.push(card2, card1);
  }
}

function combatGame(player1, player2) {
  while (!gameIsFinished(player1, player2)) {
    combatRound(player1, player2);
  }

  return player1.length ? player1 : player2;
}

parts.part1 = async function() {
  const [player1, player2] = parseInput(await readInput());
  const winner = combatGame(player1, player2);
  return countScore(winner);
};

function getSummary(player1, player2) {
  return `${player1.join('')}-${player2.join('')}`
}

function recursiveCombatGame(player1, player2, gameDepth = 0) {
  const previousGames = new Set();

  while (!gameIsFinished(player1, player2)) {
    const gameSummary = getSummary(player1, player2);
    if (previousGames.has(gameSummary)) {
      return {
        player: 1,
        cards: player1,
      };
    }
    previousGames.add(gameSummary);

    const card1 = player1.shift();
    const card2 = player2.shift();

    if (card1 <= player1.length && card2 <= player2.length) {
      const winner = recursiveCombatGame(player1.slice(0, card1), player2.slice(0, card2), gameDepth + 1);
      if (winner.player === 1) {
        player1.push(card1, card2);
      } else if (winner.player === 2) {
        player2.push(card2, card1);
      }
    }
    else if (card1 > card2) {
      player1.push(card1, card2);
    } else if (card2 > card1) {
      player2.push(card2, card1);
    }
  }

  if (player1.length) {
    return {
      player: 1,
      cards: player1,
    };
  } else if (player2.length) {
    return {
      player: 2,
      cards: player1,
    };
  }
}

parts.part2 = async function() {
  const [player1, player2] = parseInput(await readInput());
  const winner = recursiveCombatGame(player1, player2);
  return countScore(winner.cards);
};
