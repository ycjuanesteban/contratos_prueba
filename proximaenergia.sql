--CREATE DATABASE proximaenergia WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';

\connect proximaenergia

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

COMMENT ON SCHEMA public IS 'standard public schema';

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 16457)
-- Name: contracts; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.contracts (
    id uuid NOT NULL,
    user_id uuid NOT NULL,
    rate_id uuid NOT NULL,
    hiring_date timestamp without time zone NOT NULL
);


--
-- TOC entry 218 (class 1259 OID 16450)
-- Name: rates; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.rates (
    id uuid NOT NULL,
    name text NOT NULL
);


--
-- TOC entry 217 (class 1259 OID 16443)
-- Name: users; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.users (
    id uuid NOT NULL,
    name text NOT NULL,
    last_name text NOT NULL,
    dni text NOT NULL
);


--
-- TOC entry 3372 (class 0 OID 16457)
-- Dependencies: 219
-- Data for Name: contracts; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.contracts VALUES ('540bd6bc-07ea-4e88-b27f-4c9f8659ef94', '5a2ec55e-7788-4a72-b1c1-d1c46096cf9e', '472505b6-ba85-4db7-835a-f581652feabe', '2025-06-27 00:00:00');
INSERT INTO public.contracts VALUES ('d2b9a67f-b4fd-4c7d-a1aa-51c276990be3', '5a2ec55e-7788-4a72-b1c1-d1c46096cf9e', '472505b6-ba85-4db7-835a-f581652feabe', '2025-06-26 00:00:00');
INSERT INTO public.contracts VALUES ('d54d1ff6-ad9b-4105-a953-a6b95e3d732e', '5a2ec55e-7788-4a72-b1c1-d1c46096cf9e', 'a1faa7d9-4da9-49aa-b037-b29c3fbf9604', '2026-05-01 00:00:00');
INSERT INTO public.contracts VALUES ('a6b8769c-ac7c-4124-aa2a-c12bc42047a1', '10784912-73c9-4030-9cd2-4b47702fb118', '472505b6-ba85-4db7-835a-f581652feabe', '0001-10-02 00:00:00');
INSERT INTO public.contracts VALUES ('a603f6f3-9812-4734-8255-dd36b2f3ff51', '00797ad6-0819-4fa4-a2f6-ffe54b6d68b9', '472505b6-ba85-4db7-835a-f581652feabe', '2026-07-16 00:00:00');
INSERT INTO public.contracts VALUES ('c62fb7eb-08e2-431e-b7f0-05881768bf56', '5a2ec55e-7788-4a72-b1c1-d1c46096cf9e', '472505b6-ba85-4db7-835a-f581652feabe', '2025-06-14 00:00:00');
INSERT INTO public.contracts VALUES ('dbb110ac-04eb-489a-b474-ceb8174442bb', '10b8be75-d59d-471d-b331-38f546c60b73', 'bda8c2eb-3d9e-4864-9e25-60168e67c788', '2025-06-26 00:00:00');
INSERT INTO public.contracts VALUES ('5f5477ba-385b-4a46-bda2-9926b6edbe8d', '5a2ec55e-7788-4a72-b1c1-d1c46096cf9e', '472505b6-ba85-4db7-835a-f581652feabe', '2025-10-08 00:00:00');
INSERT INTO public.contracts VALUES ('c90af666-e0d8-44a7-a2e5-88d7e6048564', '10b8be75-d59d-471d-b331-38f546c60b73', '5ba1475b-77c0-4304-97c5-a7bbade06909', '2025-01-01 00:00:00');
INSERT INTO public.contracts VALUES ('941616fd-ba9d-4a2e-831b-272facb206f1', '10b8be75-d59d-471d-b331-38f546c60b73', '441d2be8-5c0b-463d-a4f8-16c9751fafa4', '2025-06-27 00:00:00');


--
-- TOC entry 3371 (class 0 OID 16450)
-- Dependencies: 218
-- Data for Name: rates; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.rates VALUES ('472505b6-ba85-4db7-835a-f581652feabe', 'Tarifa Basica 01');
INSERT INTO public.rates VALUES ('a1faa7d9-4da9-49aa-b037-b29c3fbf9604', 'Tarifa Basica 02');
INSERT INTO public.rates VALUES ('441d2be8-5c0b-463d-a4f8-16c9751fafa4', 'Tarifa Basica 03');
INSERT INTO public.rates VALUES ('5ba1475b-77c0-4304-97c5-a7bbade06909', 'Tarifa Premium 01');
INSERT INTO public.rates VALUES ('bda8c2eb-3d9e-4864-9e25-60168e67c788', 'Tarifa Premium 02');


--
-- TOC entry 3370 (class 0 OID 16443)
-- Dependencies: 217
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.users VALUES ('5a2ec55e-7788-4a72-b1c1-d1c46096cf9e', 'Jon', 'Doe', '00000001A');
INSERT INTO public.users VALUES ('00797ad6-0819-4fa4-a2f6-ffe54b6d68b9', 'Bruce', 'Banner', '00000001B');
INSERT INTO public.users VALUES ('16347977-c64a-48ed-aa2f-4056bbca9857', 'Tony', 'Stark', '00000001C');
INSERT INTO public.users VALUES ('10784912-73c9-4030-9cd2-4b47702fb118', 'Doctor ', 'Strange', '00000001D');
INSERT INTO public.users VALUES ('045704cc-0900-4c53-a995-660e5d9a7220', 'Capitana ', 'Marvel', '00000001E');
INSERT INTO public.users VALUES ('10b8be75-d59d-471d-b331-38f546c60b73', 'Black ', 'Widow', '00000001F');


--
-- TOC entry 3222 (class 2606 OID 16461)
-- Name: contracts contracts_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT contracts_pkey PRIMARY KEY (id);


--
-- TOC entry 3220 (class 2606 OID 16456)
-- Name: rates rates_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.rates
    ADD CONSTRAINT rates_pkey PRIMARY KEY (id);


--
-- TOC entry 3218 (class 2606 OID 16449)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 3223 (class 2606 OID 16467)
-- Name: contracts fk_contracts_rates; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT fk_contracts_rates FOREIGN KEY (rate_id) REFERENCES public.rates(id);


--
-- TOC entry 3224 (class 2606 OID 16462)
-- Name: contracts fk_contracts_users; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.contracts
    ADD CONSTRAINT fk_contracts_users FOREIGN KEY (user_id) REFERENCES public.users(id);


-- Completed on 2025-06-13 09:12:39

--
-- PostgreSQL database dump complete
--

