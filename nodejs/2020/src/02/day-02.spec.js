'use strict';
const {part1, part2} = require('./day-02');

test('part 1', async () => {
  expect(await part1()).toBe(424);
});

test('part 2', async () => {
  expect(await part2()).toBe(747);
});
