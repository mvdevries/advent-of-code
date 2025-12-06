'use strict';
const {part1, part2} = require('./day-10');

test('part 1', async () => {
  expect(await part1()).toBe(2277);
});

test('part 2', async () => {
  expect(await part2()).toBe(37024595836928);
});
