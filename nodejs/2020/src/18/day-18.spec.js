'use strict';
const {part1, part2} = require('./day-18');

test('part 1', async () => {
  expect(await part1()).toBe(800602729153);
});

test('part 2', async () => {
  expect(await part2()).toBe(92173009047076);
});
