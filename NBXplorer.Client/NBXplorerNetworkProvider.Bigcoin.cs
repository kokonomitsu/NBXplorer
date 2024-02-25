using NBitcoin;
using NBitcoin.DataEncoders;
using System.Collections.Generic;
using System;
using System.Net.NetworkInformation;
using static NBitcoin.Consensus;
using static NBitcoin.Altcoins.Litecoin;

namespace NBXplorer
{
    public partial class NBXplorerNetworkProvider
    {
		

		private void InitBigcoin(ChainName networkType)
		{
			NetworkBuilder networkBuilder = new NetworkBuilder();
			
			if (networkType == ChainName.Mainnet)
			{

				networkBuilder = CreateMainnet();



			}
			else if(networkType == ChainName.Testnet)
			{

				networkBuilder = CreateTestnet();
			}
			else
			{
				networkBuilder = CreateRegtest();
			}
			
			Add(new NBXplorerNetwork(networkBuilder.BuildAndRegister().NetworkSet, networkType)
			{
				MinRPCVersion = 150000,
				CoinType = networkType == ChainName.Mainnet ? new KeyPath("0'") : new KeyPath("1'")
			});
		}

		public NBXplorerNetwork GetBBTC()
		{
			return GetFromCryptoCode("BBTC");
		}

		private NetworkBuilder CreateMainnet()
		{
			var consensusFactory = new ConsensusFactory();
		

			var bech32 = Encoders.Bech32("bc");
			NetworkBuilder builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				BIP34Hash = new uint256("fa09d204a83a768ed5a7c8d441fa62f2043abf420cff1226c7b4329aeb9d51cf"),
				PowLimit = new Target(new uint256("00000fffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
				PowAllowMinDifficultyBlocks = false,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 6048,
				MinerConfirmationWindow = 8064,
				CoinbaseMaturity = 100,
				LitecoinWorkCalculation = true,
				ConsensusFactory = LitecoinConsensusFactory.Instance,
				SupportSegwit = true,
				SupportTaproot = true
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 48 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 50 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 176 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x88, 0xB2, 0x1E })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x88, 0xAD, 0xE4 })
			.SetNetworkStringParser(new LitecoinMainnetAddressStringParser())
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, bech32)
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, bech32)
			.SetBech32(Bech32Type.TAPROOT_ADDRESS, bech32)
			.SetMagic(0xdbb6c0fb)
			.SetPort(9333)
			.SetRPCPort(9332)
			.SetName("main")
			.AddAlias("mainnet")
			.AddAlias("bigcoin-mainnet")
			.AddAlias("bigccoin-main")
			.SetUriScheme("bigcoin")
			//.AddDNSSeeds(new[]
			//{
			//	new DNSSeedData("loshan.co.uk", "seed-a.litecoin.loshan.co.uk"),
			//	new DNSSeedData("thrasher.io", "dnsseed.thrasher.io"),
			//	new DNSSeedData("litecointools.com", "dnsseed.litecointools.com"),
			//	new DNSSeedData("litecoinpool.org", "dnsseed.litecoinpool.org"),
			//	new DNSSeedData("koin-project.com", "dnsseed.koin-project.com"),
			//})
			//.AddSeeds(ToSeed(pnSeed6_main))
			.SetGenesis("0x00000000f565ea242bb85208172177fd0059344d581e48e2c472adfe78ea34d9");
			return builder;
		}

		private NetworkBuilder CreateTestnet()
		{
			var bech32 = Encoders.Bech32("tb");
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 840000,
				MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 1000,
				PowLimit = new Target(new uint256("00000000000000000fffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowAllowMinDifficultyBlocks = true,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 1512,
				MinerConfirmationWindow = 2016,
				CoinbaseMaturity = 100,
				LitecoinWorkCalculation = true,
				ConsensusFactory = LitecoinConsensusFactory.Instance,
				SupportSegwit = true,
				SupportTaproot = true
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, bech32)
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, bech32)
			.SetBech32(Bech32Type.TAPROOT_ADDRESS, bech32)
			.SetMagic(0x0709110B)
			.SetPort(16333)
			.SetRPCPort(16332)
			.SetName("test")
			.AddAlias("testnet")
			.AddAlias("bigcoin-test")
			.AddAlias("bigcoin-testnet")
			.SetUriScheme("bigcoin")
			//.AddDNSSeeds(new[]
			//{
			//	new DNSSeedData("litecointools.com", "testnet-seed.litecointools.com"),
			//	new DNSSeedData("loshan.co.uk", "seed-b.litecoin.loshan.co.uk"),
			//	new DNSSeedData("thrasher.io", "dnsseed-testnet.thrasher.io"),
			//})
			//.AddSeeds(ToSeed(pnSeed6_test))
			.SetGenesis("0x0000000069922066d073e9646a13a6410655dd783e4d1d22d373cd8499b55044");
			return builder;
		}

		private NetworkBuilder CreateRegtest()
		{
		
			var bech32 = Encoders.Bech32("tb");
			var builder = new NetworkBuilder();
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 150,
				MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 144,
				PowLimit = new Target(new uint256("7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(3.5 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(2.5 * 60),
				PowAllowMinDifficultyBlocks = true,
				MinimumChainWork = uint256.Zero,
				PowNoRetargeting = true,
				RuleChangeActivationThreshold = 108,
				MinerConfirmationWindow = 2016,
				CoinbaseMaturity = 100,
				LitecoinWorkCalculation = true,
				ConsensusFactory = CreateConsensus(),
				SupportSegwit = true,
				SupportTaproot = true
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, bech32)
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, bech32)
			.SetBech32(Bech32Type.TAPROOT_ADDRESS, bech32)
			.SetMagic(0xdab5bffa)
			.SetPort(19444)
			.SetRPCPort(19443)
			.SetName("reg")
			.AddAlias("regtest")
			.AddAlias("bigcoin-reg")
			.AddAlias("bigcoin-regtest")
			.SetUriScheme("bigcoin")
			.SetGenesis("0x0000000069922066d073e9646a13a6410655dd783e4d1d22d373cd8499b55044");
			return builder;
		}

		private ConsensusFactory CreateConsensus()
		{
			var consensusFactory = new ConsensusFactory();
			return consensusFactory;
		}
	}
}

