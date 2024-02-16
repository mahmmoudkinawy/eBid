import { Auction, PagedResult } from '@/types';
import React from 'react';
import AuctionCard from './AuctionCard';

async function getData(): Promise<PagedResult<Auction>> {
  const res = await fetch('http://localhost:6001/search');

  if (!res.ok) {
    throw new Error('Error fetching data');
  }

  return res.json();
}

export default async function ListAuctions() {
  const data = await getData();

  return (
    <div className='grid grid-cols-4 gap-6'>
      {data &&
        data.results.map((auction) => (
          <AuctionCard auction={auction} key={auction.id} />
        ))}
    </div>
  );
}
