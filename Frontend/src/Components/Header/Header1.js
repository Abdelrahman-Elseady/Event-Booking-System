import React, { useState, useEffect } from "react";
import {
  AppBar,
  Toolbar,
  Typography,
  Box,
  IconButton,
  Badge,
  Button,
  Drawer,
  List,
  ListItem,
  ListItemText,
  useTheme,
  useMediaQuery,
} from "@mui/material";
import { ShoppingCart } from "@mui/icons-material";
import MenuIcon from "@mui/icons-material/Menu";
import { Link } from "react-router-dom";

export default function Header1() {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  const [drawerOpen, setDrawerOpen] = useState(false);

  const handleDrawerToggle = () => {
    setDrawerOpen((prev) => !prev);
  };

  const navItems = [
    { text: "Home", path: "/" },
    { text: "My Events", path: "/MyEvents" },
    { text: "My Calendar", path: "/Calender" },
    { text: "About Us", path: "/AboutUs" },
  ];

  return (
    <AppBar position="static">
      <Toolbar sx={{ justifyContent: "space-between" }}>
        {/* 🧭 Left Side (Logo + Menu) */}
        <Box display="flex" alignItems="center" gap={1}>
          {isMobile && (
            <IconButton color="inherit" onClick={handleDrawerToggle}>
              <MenuIcon />
            </IconButton>
          )}
          <Typography
            variant="h6"
            fontWeight="bold"
            component={Link}
            to="/"
            color="inherit"
            sx={{ textDecoration: "none" }}
          >
            Eventy
          </Typography>
        </Box>

        {/* 🖥 Center Nav (Only if not mobile) */}
        {!isMobile && (
          <Box display="flex" gap={2}>
            {navItems.map(({ text, path }) => (
              <Button key={text} component={Link} to={path} color="inherit">
                {text}
              </Button>
            ))}
          </Box>
        )}

        {/* 🛒 Right Side (Cart + Auth Buttons) */}
        <Box display="flex" alignItems="center" gap={2}>
          <IconButton component={Link} to="/Cart" color="inherit">
            <Badge badgeContent={4} color="error">
              <ShoppingCart />
            </Badge>
          </IconButton>
          <Button
            component={Link}
            to="/Login"
            variant="outlined"
            color="inherit"
            sx={{
              borderRadius: 20,
              "&:hover": { backgroundColor: "blueviolet" },
            }}
          >
            Login
          </Button>
          <Button
            component={Link}
            to="/SignUp"
            variant="outlined"
            color="inherit"
            sx={{
              borderRadius: 20,
              "&:hover": { backgroundColor: "blueviolet" },
            }}
          >
            Sign Up
          </Button>
        </Box>
      </Toolbar>

      {/* 📱 Drawer for Mobile Nav */}
      <Drawer anchor="left" open={drawerOpen} onClose={handleDrawerToggle}>
        <List sx={{ width: 200 }}>
          {navItems.map(({ text, path }) => (
            <ListItem
              button
              key={text}
              component={Link}
              to={path}
              onClick={handleDrawerToggle}
            >
              <ListItemText primary={text} />
            </ListItem>
          ))}
        </List>
      </Drawer>
    </AppBar>
  );
}
