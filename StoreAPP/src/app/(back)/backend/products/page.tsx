import React from "react";
import ProductsPage from "./Products";
import { Metadata } from "next";

type Props = {};

export const metadata: Metadata = {
  title: "Products",
  description: "Products page description",
  keywords: ["Products", "Next.js"],
};

const Products = (props: Props) => {
  return <ProductsPage />;
};

export default Products;
