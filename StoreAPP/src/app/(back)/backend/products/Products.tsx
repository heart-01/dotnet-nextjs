"use client";

import React from "react";
import DashboardCard from "@/app/components/back/shared/DashboardCard";
import { Typography, Box } from "@mui/material";

type Props = {};

const ProductsPage = (props: Props) => {
  return (
    <>
      <Box mt={2}>
        <DashboardCard title="Products">
          <Typography>This is a product page</Typography>
        </DashboardCard>
      </Box>

      <Box mt={2}>
        <DashboardCard title="Summary">
          <>
            <Typography>
              Laborum pariatur commodo adipisicing aliqua cupidatat ea ut. Do incididunt dolore laborum dolore deserunt. Deserunt ut aliquip non dolore adipisicing enim ad ea exercitation nulla cillum
              deserunt. Sunt occaecat consectetur velit commodo laboris eu tempor nostrud quis laboris esse. Id consequat dolor est incididunt ullamco ea ut.
            </Typography>

            <Typography mt={2}>
              Magna voluptate nostrud proident et officia nulla culpa dolore. Nostrud ea amet mollit duis et officia proident qui duis minim esse veniam elit. Excepteur cillum minim tempor duis
              deserunt eiusmod pariatur do ex exercitation magna.
            </Typography>
          </>
        </DashboardCard>
      </Box>
    </>
  );
};

export default ProductsPage;
