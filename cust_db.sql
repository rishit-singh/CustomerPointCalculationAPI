--
-- PostgreSQL database dump
--

-- Dumped from database version 14.3
-- Dumped by pg_dump version 14.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: test; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test (
    id character(5)
);


ALTER TABLE public.test OWNER TO postgres;

--
-- Name: transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transactions (
    id character(64) NOT NULL,
    userid character(64) NOT NULL,
    amount integer,
    datetime character varying(32)
);


ALTER TABLE public.transactions OWNER TO postgres;

--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id character(64),
    name character varying(64)
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Data for Name: test; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.test (id) FROM stdin;
\.


--
-- Data for Name: transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transactions (id, userid, amount, datetime) FROM stdin;
e93863e897b4bee341952df21e80f2eb2baa8027953f8a0a8c4a6786d2a57b72	efc0509e0aef7f2e5a2ad5dd46f3c2c6e4c237b58045df328dac9e937170408f	7697	6/1/2022 12:30:34 AM
69c4fe966fd8c8742e09b0a1479180acb6cd732db04846bfa6e27d2bfe778107	efc0509e0aef7f2e5a2ad5dd46f3c2c6e4c237b58045df328dac9e937170408f	8017	5/8/2022 12:30:59 AM
d341f434fdd1d3bf1aa6bfa882cd533133db3bf83f33c98f3b9ee302e201403c	ecec8027869cc30801cf8f8cf7c2e11c692dc2cd7cc9948327fe4d65818a714b	2255	9/22/2022 12:31:01 AM
cfc34d0ca335c2e4dea2d154078b81b8a5cebe5674d9f35ef80516c5d3753976	282b7bcd8d65d31b62685fc8f903330b7fcef6b41c3a2d696f1f26c7cdf84019	9973	11/25/2022 12:31:03 AM
3928523af8604fd14c52e2e54cd2760adf4c2d353b2fa9dbc676584ee04817cd	ffa4d7ec52c58f20da88aa17214c03af5c93f657a247f5ed1e3c5cf433b37117	9203	4/1/2022 12:31:06 AM
a978533fe10c737e60e62d2768776dcb4c2d6259644f3bdee4de9760696f595a	efc0509e0aef7f2e5a2ad5dd46f3c2c6e4c237b58045df328dac9e937170408f	6943	10/1/2022 12:31:09 AM
6ddde6ac51f9f7fc5be1906ad4fd36bbd4d49c80e840f0f8c1f42436070ec258	282b7bcd8d65d31b62685fc8f903330b7fcef6b41c3a2d696f1f26c7cdf84019	7486	9/23/2022 12:31:11 AM
cb52812cc2b8492f55523ea91c2d01966f5679e2b8822cad5403fe71243a9045	8d942a2867a52edce3205254aaadaa3fe3c668e1e128ed137570febcd6f7876a	929	6/1/2022 12:31:14 AM
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, name) FROM stdin;
ffa4d7ec52c58f20da88aa17214c03af5c93f657a247f5ed1e3c5cf433b37117	foobar_381
8d942a2867a52edce3205254aaadaa3fe3c668e1e128ed137570febcd6f7876a	foobar_482
d7f8cb128279dcac88ffcb8f6cfb1fcc798f5afd4345a75ff12d3e446d06d78c	foobar_38
282b7bcd8d65d31b62685fc8f903330b7fcef6b41c3a2d696f1f26c7cdf84019	foobar_171
364e6a412cbe0520cf299852bb1dc3c2212327472c99018d970740370862c73b	foobar_985
efc0509e0aef7f2e5a2ad5dd46f3c2c6e4c237b58045df328dac9e937170408f	foobar_969
e36e80701b8a18a57d2c1ebd740ffb365a7b13dc02160a61d46dabacffec4709	foobar_615
ecec8027869cc30801cf8f8cf7c2e11c692dc2cd7cc9948327fe4d65818a714b	foobar_351
\.


--
-- Name: transactions transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_pkey PRIMARY KEY (id);


--
-- PostgreSQL database dump complete
--

