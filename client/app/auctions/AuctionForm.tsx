'use client';
import React from 'react';
import { FieldValues, useForm } from 'react-hook-form';
import Input from '../components/Input';
import { Button } from 'flowbite-react';

export default function AuctionForm() {
  const {
    control,
    handleSubmit,
    formState: { isSubmitted, isValid, isDirty, errors },
  } = useForm();

  function onSubmit(data: FieldValues) {
    console.log(data);
  }

  return (
    <form className='flex flex-col mt-3' onSubmit={handleSubmit(onSubmit)}>
      <Input
        label='Make'
        name='make'
        control={control}
        rules={{ required: 'Make is required' }}
      />
      <Input
        label='Model'
        name='model'
        control={control}
        rules={{ required: 'Model is required' }}
      />
      <div className='flex justify-between'>
        <Button outline color='gray'>
          Cancel
        </Button>
        <Button
          type='submit'
          isProcessing={isSubmitted}
          outline
          color='success'
        >
          Submit
        </Button>
      </div>
    </form>
  );
}
