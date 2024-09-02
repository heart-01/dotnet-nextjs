"use client";

import React from "react";
import { Box, Grid } from "@mui/material";
import Blog from "@/app/components/back/dashboard/Blog";
import MonthlyEarnings from "@/app/components/back/dashboard/MonthlyEarnings";
import ProductPerformance from "@/app/components/back/dashboard/ProductPerformance";
import RecentTransactions from "@/app/components/back/dashboard/RecentTransactions";
import SalesOverview from "@/app/components/back/dashboard/SalesOverview";
import YearlyBreakup from "@/app/components/back/dashboard/YearlyBreakup";

type Props = {};

const DashboardPage = (props: Props) => {
  return (
    <Box mt={3}>
      <Grid container spacing={3}>
        <Grid item xs={12} lg={8}>
          <SalesOverview />
        </Grid>
        <Grid item xs={12} lg={4}>
          <Grid container spacing={3}>
            <Grid item xs={12}>
              <YearlyBreakup />
            </Grid>
            <Grid item xs={12}>
              <MonthlyEarnings />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={12} lg={4}>
          <RecentTransactions />
        </Grid>
        <Grid item xs={12} lg={8}>
          <ProductPerformance />
        </Grid>
        <Grid item xs={12}>
          <Blog />
        </Grid>
      </Grid>
    </Box>
  );
};

export default DashboardPage;
