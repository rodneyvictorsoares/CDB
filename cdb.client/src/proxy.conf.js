
const target = "https://localhost:7226";

const PROXY_CONFIG = [
  {
    context: [
      "/api/cdb"
    ],
    target,
    secure: false,
    changeOrigin: true
  }
]

module.exports = PROXY_CONFIG;
