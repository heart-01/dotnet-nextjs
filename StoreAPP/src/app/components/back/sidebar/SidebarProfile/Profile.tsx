import Avatar from "@mui/material/Avatar";
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import Tooltip from "@mui/material/Tooltip";
import Typography from "@mui/material/Typography";
import useMediaQuery from "@mui/material/useMediaQuery";
import { IconPower } from "@tabler/icons-react";
import Link from "next/link";

export const Profile = () => {
  const lgUp = useMediaQuery((theme: any) => theme.breakpoints.up("lg"));
  const hideMenu = false;

  return (
    <Box display={"flex"} alignItems="center" gap={2} sx={{ m: 3, p: 2, bgcolor: `${"primary.light"}` }}>
      {!hideMenu && (
        <>
          <Avatar alt="Remy Sharp" src={"/images/profile/user-1.jpg"} sx={{ height: 40, width: 40 }} />
          <Box>
            <Typography variant="h6">Mathew</Typography>
            <Typography variant="caption">Designer</Typography>
          </Box>
          <Box sx={{ ml: "auto" }}>
            <Tooltip title="Logout" placement="top">
              <IconButton color="error" component={Link} href="/login" aria-label="logout" size="small">
                <IconPower size="20" />
              </IconButton>
            </Tooltip>
          </Box>
        </>
      )}
    </Box>
  );
};
