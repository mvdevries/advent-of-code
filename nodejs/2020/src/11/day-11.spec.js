'use strict';
const {part1, part2} = require('./day-11');

test('part 1', async () => {
  expect(await part1()).toBe(2424);
});

test('part 2', async () => {
  expect(await part2()).toBe(2208);
});
