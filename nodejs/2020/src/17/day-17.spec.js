'use strict';
const {part1, part2} = require('./day-17');

test('part 1', async () => {
  expect(await part1()).toBe(375);
});

test('part 2', async () => {
  expect(await part2()).toBe(2192);
});
