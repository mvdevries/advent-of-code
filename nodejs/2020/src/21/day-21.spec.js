'use strict';
const {part1, part2} = require('./day-21');

test('part 1', async () => {
  expect(await part1()).toBe(2659);
});

test('part 2', async () => {
  expect(await part2()).toBe('rcqb,cltx,nrl,qjvvcvz,tsqpn,xhnk,tfqsb,zqzmzl');
});
