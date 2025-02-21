# E-Commerce Project

this is just a school project, nothing serious

This project is an e-commerce application that manages customer, product and order data. 
It allows the addition of customers and products via XML files. The project leverages basic
data management and XML parsing to integrate information into the system.

# Setup instructions

1. download the project from releases
2. run database.sql script in your Microsoft SQL server instance
3. configure the E-commerce\net6.0\e-commerce.dll.config file to match your Microsoft SQL server instance details
4. done


XML examples: 

customer:
```
<?xml version="1.0" encoding="UTF-8"?>
<Customers>
  <Customer>
    <name>John Doe</name>
    <email>johndoe@example.com</email>
    <phone>123-456-7890</phone>
    <address>...</address>
    <registration_date>2024-01-01</registration_date>
  </Customer>
</Customers>

```

Product:
```
<?xml version="1.0" encoding="UTF-8"?>
<Products>
  <Product>
    <name>Phone X</name>
    <desc>OLED display phone</desc>
    <price>999</price>
    <stock>50</stock>
  </Product>
</Products>
```
