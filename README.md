# Bakery Shopping Cart Application

## Overview

This is a shopping cart application designed to handle product pricing based on unit prices, volume pricing, and sales events. The application allows users to specify a date for the purchase, and it calculates the total price for the items in the cart accordingly.

## Features

- Supports unit and volume pricing for products.
- Implements special sales based on specific dates.
- Calculates the correct total price based on sales and applicable pricing rules.

### Products

- **Cookies**: $1.25 each or special pricing on Fridays (8 cookies for $6.00).
- **Brownies**: $2.00 each.
- **Cheesecakes**: $5.00 each (25% off on October 1st).
- **Mini Gingerbread Donuts**: $1.50 each (two for one on Tuesdays).

### Sales

- **Every Friday**: 8 Cookies for $6.00.
- **Every October 1st**: Any number of Key Lime Cheesecake at 25% off.
- **Every Tuesday**: Mini Gingerbread Donut two for one.

## Test Cases

Here are some inputs you can use to test the application:

1. **Without any sales active**: 
   - Add: 1 cookie, 4 brownies, 1 cheesecake.
   - **Total Price**: $16.25

2. **Without any sales active**: 
   - Add: 8 cookies.
   - **Total Price**: $8.50

3. **Without any sales active**: 
   - Add: 1 cookie, 1 brownie, 1 cheesecake, 2 donuts.
   - **Total Price**: $12.25

4. **On October 1, 2022**: 
   - Add: 8 cookies, 4 cheesecakes.
   - **Total Price**: $32.50

## Getting Started

### Installation

1. Clone the repository:
2. Run docker-compose up -d

## Worth mentioning

1. There were at least 3 different strategies that could be applied to handle multiple sales one product:
 - Add prioritization for each sale and apply based on it.
 - Apply first random sale
 - Apply only bulk pricing or sale.
 - Find the best deal for customer and apply it (chosen one).

2. A lot of stuff in current implementation is not the best from UI/UX perspective, a lot of small features can be applied like:
 - Suggest customer to add certain amount of products to apply sale.
 - Highlight sales based on date directly on products page.
