#!/bin/sh
sed -i 's/Theory/Test/g' ./**/*
sed -i 's/Fact/Test/g' ./**/*
sed -i 's/InlineData/TestCase/g' ./**/*
sed -i 's/JikanValidationException/AostaValidationException/g' ./**/*
sed -i 's/_jikan/JikanSetup.Instance/g' ./**/* 


