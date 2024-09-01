import React from "react";
import { Metadata } from "next";
import LoginPage from "./Login";

type Props = {};

export const metadata: Metadata = {
  title: "Login",
  description: "Login page description",
  keywords: ["Login", "Next.js"],
};

const Login = (props: Props) => {
  return <LoginPage />;
};

export default Login;
