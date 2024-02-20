'use server';

import { PagedResult, Auction } from '@/types';

export async function getData(
  pageNumber: number,
  pageSize: number
): Promise<PagedResult<Auction>> {
  const res = await fetch(
    `http://localhost:6001/search?pageNumber=${pageNumber}&pageSize=${pageSize}`
  );

  if (!res.ok) {
    throw new Error('Error fetching data');
  }

  return res.json();
}
