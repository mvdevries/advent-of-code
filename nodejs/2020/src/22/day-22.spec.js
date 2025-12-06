'use strict';
const {part1, part2} = require('./day-22');

test('part 1', async () => {
  expect(await part1()).toBe(33403);
});

test('part 2', async () => {
  expect(await part2()).toBe(29177);
});
