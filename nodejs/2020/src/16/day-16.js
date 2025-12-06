'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

// extract list of fields and requirements
function parseFields(fields) {
  return fields.split('\n').map(field => {
    const [, name, min1, max1, min2, max2] = field.match(/([a-z ]+): (\d+)-(\d+) or (\d+)-(\d+)/);
    return {
      name,
      options: [
        {min: parseInt(min1), max: parseInt(max1)},
        {min: parseInt(min2), max: parseInt(max2)},
      ],
    };
  });
}

function parseMyTicket(myTicket) {
  return myTicket.split('\n')[1].split(',').map(field => parseInt(field));
}

function parseNearTickets(nearTickets) {
  return nearTickets.split('\n').slice(1).map(t => t.split(',').map(field => parseInt(field)))
}

function parseInput(input) {
  const [
    fields,
    myTicket,
    nearTickets,
  ] = input.split('\n\n');

  return {
    fields: parseFields(fields),
    myTicket: parseMyTicket(myTicket),
    nearTickets: parseNearTickets(nearTickets),
  };
}


parts.part1 = async function() {
  const input = parseInput(await readInput());

  const validFieldRanges = input.fields.map(field => field.options).flat();
  const invalidFieldValues = input.nearTickets
    .flat()
    .filter(value => validFieldRanges.every(option => value < option.min || value > option.max));

  return invalidFieldValues.reduce((sum, value) => sum + value, 0);
};

parts.part2 = async function() {
  const input = parseInput(await readInput());

  const validFieldRanges = input.fields.map(field => field.options).flat();
  const invalidFieldValues = input.nearTickets
    .flat()
    .filter(value => validFieldRanges.every(option => value < option.min || value > option.max));

  const allValidTickets = input.nearTickets
    .filter(ticket => !ticket.some(value => invalidFieldValues.includes(value)))
    .concat([ input.myTicket ]);

  // compute field names
  const fieldPositions = new Map();
  const remainingFields = Array.from(input.fields);
  const remainingFieldPositions = remainingFields.map((_, i) => i);

// loop while we have fields, since we have to progressively narrow the search space
  while (remainingFields.length > 0) {
    let didFindAMatch = false;

    // loop through each field by position
    for (const i of remainingFieldPositions) {
      // get list of fields for this position
      const values = allValidTickets.map(t => t[i]).flat();

      // find all possible matches for this field
      const matchingFields = remainingFields.filter(field => {
        return values.every(value => {
          return field.options.some(option => {
            return value >= option.min && value <= option.max;
          });
        });
      });

      if (matchingFields.length === 0) {
        throw new Error(`Did not fid a match for field ${ i }`);
      }

      // if there is exactly one, then we found the match
      if (matchingFields.length === 1) {
        // save field position
        const matchingField = matchingFields[0];
        fieldPositions.set(matchingField.name, i);

        // mark field and position as consumed
        remainingFields.splice(remainingFields.indexOf(matchingField), 1);
        remainingFieldPositions.splice(remainingFieldPositions.indexOf(i), 1);

        // keep iterating - we haven't failed yet
        didFindAMatch = true;
      }
    }

    // detect and break out of stalls
    if (!didFindAMatch) {
      throw new Error(`Search stalled - no new matches found and ${ remainingFields.length } remaining.`);
    }
  }

  return [
    'departure location',
    'departure station',
    'departure platform',
    'departure track',
    'departure date',
    'departure time',
  ]
    .map(field => fieldPositions.get(field))
    .map(fieldPosition => input.myTicket[fieldPosition])
    .reduce((prod, value) => prod * value, 1);
};
