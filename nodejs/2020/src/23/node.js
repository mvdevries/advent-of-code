'use strict';

module.exports = class Node {
  constructor(value, next = null) {
    this.value = value;
    this.next = next;
  }
}
