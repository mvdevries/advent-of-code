'use strict';
const {part1, part2} = require('./day-20');

test('part 1', async () => {
  expect(await part1()).toBe(4006801655873);
});

test('part 2', async () => {
  expect(await part2()).toBe(1838);
});
