'use strict';
const {readFile} = require('fs');
const {promisify} = require('util');
const readFileAsync = promisify(readFile);

const parts = module.exports = {};

function readInput() {
  return readFileAsync(__dirname + '/input.txt', 'utf8');
}

function parseInput(input) {
  return input
    .split('\n')
    .filter(food => food)
    .map(food => {
      const [ingredients, allergens] = food.split(' (contains ');
      return {
        ingredients: new Set(ingredients.split(' ')),
        allergens: new Set(allergens.slice(0, -1).split(', ')),
      };
    });
}

function getAllIngredients(foodList) {
  const ingredients = new Set();
  for (const food of foodList) {
    for (const ingredient of food.ingredients) {
      ingredients.add(ingredient);
    }
  }

  return ingredients;
}

function createSetOfAllIngredientsForAllergens(foodList, ingredientsSet) {
  const allergensMap = new Map();

  for (const food of foodList) {
    for (const allergen of food.allergens) {
      allergensMap.set(allergen, new Set(ingredientsSet));
    }
  }

  for (const [allergen, ingredients] of allergensMap) {
    for (const food of foodList) {
      if (!food.allergens.has(allergen)) {
        continue;
      }

      for (const ingredient of new Set(ingredients)) {
        if (!food.ingredients.has(ingredient)) {
          ingredients.delete(ingredient);
        }
      }
    }
  }

  return allergensMap;
}

function getSafeIngredients(ingredients, ingredientsForAllergens) {
  const safeIngredients = new Set(ingredients);
  for (const [, ingredients] of ingredientsForAllergens) {
    for (const ingredient of ingredients) {
      safeIngredients.delete(ingredient);
    }
  }

  return safeIngredients;
}

function countIngredientAppearances(foodList, safeIngredients) {
  let appearances = 0;
  for (const food of foodList) {
    for (const ingredient of food.ingredients) {
      appearances += safeIngredients.has(ingredient);
    }
  }

  return appearances;
}

parts.part1 = async function() {
  const foodList = parseInput(await readInput());

  const ingredients = getAllIngredients(foodList);
  const ingredientsForAllergens = createSetOfAllIngredientsForAllergens(foodList, ingredients);
  const safeIngredients = getSafeIngredients(ingredients, ingredientsForAllergens);
  return countIngredientAppearances(foodList, safeIngredients);
};


parts.part2 = async function() {
  const foodList = parseInput(await readInput());
  const ingredients = getAllIngredients(foodList);
  const ingredientsForAllergens = createSetOfAllIngredientsForAllergens(foodList, ingredients);

  const list = [];
  const allergensLeft = new Set([...ingredientsForAllergens.keys()]);
  while (list.length < ingredientsForAllergens.size) {
    let delAllergen;
    let delIngredient;
    for (const allergen of allergensLeft) {
      if (ingredientsForAllergens.get(allergen).size === 1) {
        delAllergen = allergen;
        delIngredient = [...ingredientsForAllergens.get(allergen)][0];
        list.push({
          ingredient: delIngredient,
          allergen,
        });
        break;
      }
    }

    allergensLeft.delete(delAllergen);

    for (const [allergen, ingredients] of ingredientsForAllergens) {
      if (allergen !== delAllergen) {
        ingredients.delete(delIngredient);
      }
    }
  }

  list.sort((a, b) => (a.allergen > b.allergen) ? 1 : ((b.allergen > a.allergen) ? -1 : 0))
  return list.map(item => item.ingredient).join(',');
};
