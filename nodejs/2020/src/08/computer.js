'use strict';

module.exports = class Computer {
  constructor() {
    this._acc = 0;
    this._pc = 0;
    this._ppc = 0;
  }

  load(program, clearComputer) {
    this._program = program;
    if (!clearComputer) {
      this._acc = 0;
      this._pc = 0;
      this._ppc = 0;
    }
  }

  execute() {
    while (!!this.nextInstruction) {
      console.log(`${this.nextInstruction}: ${this.programCounter} ${this.accumulator}`)
      this.executeNextInstruction();
    }
  }

  executeCatchOnloop() {
    const executedInstructions = new Set();
    while (!!this.nextInstruction) {
      const uniqInstruction = `${this.programCounter}`;
      if (executedInstructions.has(uniqInstruction)) {
        return true;
      }

      executedInstructions.add(uniqInstruction);
      this.executeNextInstruction();
    }

    return false;
  }

  executeNextInstruction() {
    const {code, count} = this.parseInstruction(this._program[this._pc]);
    this[code](count);
  }

  parseInstruction(instruction) {
    const [code, count] = instruction.split(' ');
    return {
      code,
      count: parseInt(count),
    }
  }

  acc(count) {
    this._acc += count;
    this.programCounter += 1;
  }

  jmp(count) {
    this.programCounter += count;
  }

  nop(count) {
    this.programCounter += 1;
  }

  get accumulator() {
    return this._acc;
  }

  get programCounter() {
    return this._pc;
  }

  set programCounter(value) {
    this._ppc = this._pc;
    this._pc = value;
  }

  get previousProgramCounter() {
    return this._ppc;
  }

  get nextInstruction() {
    return this._program[this.programCounter];
  }

  get previousInstruction() {
    return this._program[this.previousProgramCounter];
  }
}
