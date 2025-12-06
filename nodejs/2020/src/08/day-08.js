'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);
const Computer = require('./computer');

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function toEntryArray(input) {
  return input.split('\n').filter(n => n);
}

function changeInstruction(instruction) {
  if (instruction.includes('jmp')) {
    return instruction.replace('jmp', 'nop');
  } else if (instruction.includes('nop')) {
    return instruction.replace('nop', 'jmp');
  }

  return instruction;
}

function getAlteredProgram(program, i) {
  const instruction = program[i];
  const programCopy = [...program];
  programCopy[i] = changeInstruction(instruction);

  return programCopy;
}

parts.part1 = async function() {
  const lines = toEntryArray(await readInput());

  const computer = new Computer();
  computer.load(lines);
  const hasLoop = computer.executeCatchOnloop();

  return computer.accumulator;
}

parts.part2 = async function() {
  const program = toEntryArray(await readInput());
  const computer = new Computer();

  for (let i = 0; i < program.length; i++) {
    computer.load(getAlteredProgram(program, i));

    const loopDetected = computer.executeCatchOnloop();
    if (!loopDetected) {
      console.log('no loop detected', program.accumulator);
      break;
    }
  }

  return computer.accumulator;
}
