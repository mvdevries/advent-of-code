'use strict';
const {part1, part2} = require('./day-16');

test('part 1', async () => {
  expect(await part1()).toBe(26009);
});

test('part 2', async () => {
  expect(await part2()).toBe(589685618167);
});
