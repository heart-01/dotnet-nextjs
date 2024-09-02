import React from "react";
import DashboardPage from "./Dashboard";
import { Metadata } from "next";

type Props = {};

export const metadata: Metadata = {
  title: "Dashboard",
  description: "Dashboard page description",
  keywords: ["Dashboard", "Next.js"],
};

const Dashboard = (props: Props) => {
  return <DashboardPage />;
};

export default Dashboard;
