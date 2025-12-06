'use strict';
const {part1, part2} = require('./day-01');

test('part 1', async () => {
  expect(await part1()).toBe(805731);
});

test('part 2', async () => {
  expect(await part2()).toBe(192684960);
});
