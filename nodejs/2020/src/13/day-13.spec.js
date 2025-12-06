'use strict';
const {part1, part2} = require('./day-13');

test('part 1', async () => {
  expect(await part1()).toBe(259);
});

test('part 2', async () => {
  expect(await part2()).toBe(210612924879242);
});
