'use strict';
const {part1, part2} = require('./day-14');

test('part 1', async () => {
  expect(await part1()).toBe(15919415426101);
});

test('part 2', async () => {
  expect(await part2()).toBe(3443997590975);
});
