/** @type {import('next').NextConfig} */
const nextConfig = {
  images: {
    domains: ['cdn.pixabay.com', 'media.istockphoto.com'],
  },
  logging: {
    fetches: {
      fullUrl: true,
    },
  },
  output: 'standalone',
};

module.exports = nextConfig;
