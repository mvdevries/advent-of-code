'use strict';
const {part1, part2} = require('./day-09');

test('part 1', async () => {
  expect(await part1()).toBe(32321523);
});

test('part 2', async () => {
  expect(await part2()).toBe(4794981);
});
