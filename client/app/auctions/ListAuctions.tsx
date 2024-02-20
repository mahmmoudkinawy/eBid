'use client';

import React, { useEffect, useState } from 'react';
import AuctionCard from './AuctionCard';
import PageSelector from '../components/PageSelector';
import { getData } from '../actions/auctionActions';
import { Auction, PagedResult } from '@/types';
import Filters from './Filters';
import { useParamsStore } from '@/hooks/useParamsStore';
import { shallow } from 'zustand/shallow';
import queryString from 'query-string';

export default function ListAuctions() {
  const [data, setData] = useState<PagedResult<Auction>>();
  const params = useParamsStore(
    (state) => ({
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      searchTerm: state.searchTerm,
    }),
    shallow
  );
  const setParams = useParamsStore((state) => state.setParams);
  const url = queryString.stringifyUrl({ url: '', query: params });

  function setPageNumber(pageNumber: number) {
    setParams({ pageNumber });
  }

  useEffect(() => {
    getData(url).then((data) => {
      setData(data);
    });
  }, [url]);

  if (!data) return <h3>Loading...</h3>;

  return (
    <>
      <Filters />
      <div className='grid grid-cols-4 gap-6'>
        {data &&
          data.results.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
      </div>
      <div className='flex justify-center mt-4'>
        <PageSelector
          pageChanged={setPageNumber}
          currentPage={params.pageNumber}
          pageCount={data.pageCount}
        />
      </div>
    </>
  );
}
