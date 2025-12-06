'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input.split('\n').filter(n => n);
}

const operations = {
  "+": (a, b) => a + b,
  "*": (a, b) => a * b,
};

function evaluate(expression, precedence) {
  const operators = [];
  const numbers = [];

  const calculate = () => {
    const op = operators.pop()
    const b = numbers.pop()
    const a = numbers.pop()
    numbers.push(operations[op](a, b))
  }

  for (const char of expression) {
    if (char === " ") {
      continue;
    }

    if (char.match(/\d/)) {
      numbers.push(Number(char));
      continue;
    }

    if (char === '(') {
      operators.push(char);
      continue;
    }

    if (char === ')') {
      while (operators.length > 0 && operators[operators.length - 1] !== '(') {
        calculate();
      }
      operators.pop();
      continue;
    }

    while (
      operators.length > 0 &&
      precedence[operators[operators.length - 1]] >= precedence[char]
      ) {
      calculate();
    }

    operators.push(char);
  }

  while(operators.length > 0) {
    calculate();
  }

  return numbers[0];
}

parts.part1 = async function() {
  const input = parseInput(await readInput());
  const precedence = { "+": 1, "*": 1 };

  return input
    .map(expression => evaluate(expression, precedence))
    .reduce((a, b) => a + b);
};

parts.part2 = async function() {
  const input = parseInput(await readInput());
  const precedence = { "+": 2, "*": 1 }

  return input
    .map(expression => evaluate(expression, precedence))
    .reduce((a, b) => a + b);
};
